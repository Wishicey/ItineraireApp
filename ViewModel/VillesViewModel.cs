using ItineraireApp.Models;
using ItineraireApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ItineraireApp.ViewModel
{
    class VillesViewModel
    {
        public ObservableCollection<VilleTypeResponse> cListVilles { get; set; }

        public VillesViewModel()
        {
            Task<List<VilleTypeResponse>> icTask = ApiService.GetVillesAsync();

            cListVilles = new ObservableCollection<VilleTypeResponse>(icTask.Result);
        }
    }
}
