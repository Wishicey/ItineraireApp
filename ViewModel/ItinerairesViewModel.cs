using ItineraireApp.Models;
using ItineraireApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItineraireApp.ViewModel
{
    class ItinerairesViewModel
    {
        public string sIdItineraire { get; set; }
        public List<string> cListVillesName { get; set; }
        public string sIdVille { get; set; }
        public string sVilleName { get; set; }
        public ObservableCollection<VilleTypeResponse> cListVilles { get; set; }
        public ItinerairesViewModel(int iIdVille)
        {
            if (iIdVille != 0)
            {
                Task<List<ItineraireTypeResponse>> icTaskItineraire = ApiService.GetItineraireWithVilleAsync(iIdVille);

                ObservableCollection<ItineraireTypeResponse>  cListItineraires = new ObservableCollection<ItineraireTypeResponse>(icTaskItineraire.Result);

                Task<List<VilleTypeResponse>> icTask = ApiService.GetVillesAsync();

                cListVilles = new ObservableCollection<VilleTypeResponse>(icTask.Result);

                cListVillesName = new List<string>();
                
                //On compte le nombre d'itineraires différents
                int iDistinctItineraire = cListItineraires.Select(x => x.idItineraire).Distinct().Count();
                if(iDistinctItineraire == 1)
                {
                    //tri en fonction de l'id de la ville (plus la ville a un id élévé, plus elle est proche du point d'arivée)
                    foreach (ItineraireTypeResponse icItineraire in cListItineraires.OrderBy(x => x.idVille))
                    {
                        sIdItineraire = icItineraire.idItineraire.ToString();
                        cListVillesName.Add(icItineraire.idVilleNavigation.nomVille);
                        sIdVille += icItineraire.idVille.ToString() + ", ";
                        sVilleName += icItineraire.idVilleNavigation.nomVille.ToString() + ", ";
                    }
                }else if(iDistinctItineraire > 1)
                {
                    foreach (ItineraireTypeResponse icItineraire in cListItineraires)
                    {
                        sIdItineraire += icItineraire.idItineraire.ToString()+"; ";
                        cListVillesName.Add(icItineraire.idVilleNavigation.nomVille);
                        sIdVille += icItineraire.idVille.ToString() + ", ";
                        sVilleName += icItineraire.idVilleNavigation.nomVille.ToString() + ", ";
                    }
                }
                
            }
        }
    }
}
