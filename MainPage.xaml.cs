using ItineraireApp.Models;
using ItineraireApp.Services;
using ItineraireApp.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace ItineraireApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //On positionne la carte au dessus de la france par défaut
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(46,2), Distance.FromKilometers(300)));
            BindingContext = new VillesViewModel();
        }

        public void OnItineraireClicked(object sender, EventArgs args)
        {
            //On viens effacer les trajets et "Pin" précédent lorsqu'il y a un clique sur le bouton recherche d'itineraire
            int iNbPolylines = map.Polylines.Count();
            for(int i = iNbPolylines; i > 0; i--)
            {
                map.Polylines.RemoveAt(i-1);
            }
            if (map.Pins.Count() > 0) map.Pins.RemoveAt(0);


            VilleTypeResponse icVilleSelected = (VilleTypeResponse)picker.ItemsSource[picker.SelectedIndex];
            ItinerairesViewModel icItineraireViewModel;
            BindingContext = icItineraireViewModel = new ItinerairesViewModel(icVilleSelected.idVille);


            List<GoogleMapsRoute.Root> cListGoogleMapRoot = new List<GoogleMapsRoute.Root>();
            int iDistinctItineraire = icItineraireViewModel.cListVillesName.Count();

            //On cherche à créer des itinéraires alternatifs lorsqu'ils en existe plusieurs
            cListGoogleMapRoot.Add(ApiService.GetPolylineMapsDirections(icItineraireViewModel.cListVillesName.Take(4).ToList()).Result);
            switch (iDistinctItineraire)
            {
                case 8:
                    cListGoogleMapRoot.Add(ApiService.GetPolylineMapsDirections(icItineraireViewModel.cListVillesName.Skip(4).Take(4).ToList()).Result);
                    break;
                case 12:
                    cListGoogleMapRoot.Add(ApiService.GetPolylineMapsDirections(icItineraireViewModel.cListVillesName.Skip(4).Take(4).ToList()).Result);
                    cListGoogleMapRoot.Add(ApiService.GetPolylineMapsDirections(icItineraireViewModel.cListVillesName.Skip(8).Take(4).ToList()).Result);
                    break;
            }

            List<Polyline> getPolyline(List<GoogleMapsRoute.Root> cListGoogle)
            {
                int iCountListGoogle = cListGoogle.Count();
                List<Polyline> cListPolyline = new List<Polyline>();
                //On vient ajouter les "polylines" qui serviront à tracer un trajet entre 2 points
                Polyline icPolyline = new Polyline()
                {
                    StrokeColor = Color.Blue,
                    StrokeWidth = 8,
                };
                for (int i = 0; i <= iCountListGoogle-1; i++)
                {
                    if(i > 0)
                    {
                        //On différencie les itinéraires alternatifs
                        icPolyline = new Polyline()
                        {
                            StrokeColor = Color.DarkBlue,
                            StrokeWidth = 6,
                        };
                    }
                    //Pour cela nous avons besoin de créer une position avec la latitude et la longitude
                    icPolyline.Positions.Add(new Position(cListGoogle[i].routes.FirstOrDefault().legs.FirstOrDefault().start_location.lat, cListGoogle[i].routes.FirstOrDefault().legs.FirstOrDefault().start_location.lng));
                    for (int j = 0; j < 3; j++)
                    {
                        icPolyline.Positions.Add(new Position(cListGoogle[i].routes.FirstOrDefault().legs[j].end_location.lat, cListGoogle[i].routes.FirstOrDefault().legs[j].end_location.lng));
                    }
                    cListPolyline.Add(icPolyline);
                }
                return cListPolyline;
            }

            List<Polyline> cPolyline = getPolyline(cListGoogleMapRoot);
            foreach(Polyline icPolylineUnique in cPolyline)
            {
                //On termine par ajouter ces Polylines à la carte
                map.Polylines.Add(icPolylineUnique);
            }


            //De même pour la position de la ville selectionnée
            Position icPosition = new Position();
            for (int i = 0; i < 3; i++)
            {
                if (cListGoogleMapRoot.FirstOrDefault().routes.FirstOrDefault().legs[i].start_address.Contains(icVilleSelected.nomVille))
                {
                    icPosition = new Position(cListGoogleMapRoot.FirstOrDefault().routes.FirstOrDefault().legs[i].start_location.lat, cListGoogleMapRoot.FirstOrDefault().routes.FirstOrDefault().legs[i].start_location.lng);
                }else if (cListGoogleMapRoot.FirstOrDefault().routes.FirstOrDefault().legs[i].end_address.Contains(icVilleSelected.nomVille))
                {
                    icPosition = new Position(cListGoogleMapRoot.FirstOrDefault().routes.FirstOrDefault().legs[i].end_location.lat, cListGoogleMapRoot.FirstOrDefault().routes.FirstOrDefault().legs[i].end_location.lng);
                }
            }

            //On ajoute un Pin à la position recherchée
            Pin icPin = new Pin()
            {
                Type = PinType.Place,
                Label = icVilleSelected.nomVille,
                Position = icPosition,
                Tag = "pin_searched",
            };
            map.Pins.Add(icPin);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(icPin.Position, Distance.FromKilometers(300)));
        }
    }
}
