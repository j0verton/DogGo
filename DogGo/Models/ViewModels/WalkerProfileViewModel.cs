using DogGo.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public List<Walk> Walks { get; set; }

        public Neighborhood Neighborhood { get; set; }

        public List<Owner> clientOwners { get; set; }

        public List<Dog> clientDogs { get; set; }

        //public ViewUtils viewUtils { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}")]
        public TimeSpan TotalWalkTime {
            get
            {
                int totalDurations = Walks.Sum(walk => walk.Duration);
                return new TimeSpan(0, 0, totalDurations);
            }
        }

        public List<WalkSummaryViewModel> WalkSummaries { get; set; }

    }
}
