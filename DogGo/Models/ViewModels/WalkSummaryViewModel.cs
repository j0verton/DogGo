using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkSummaryViewModel
    {
        public Walk walk;
        public TimeSpan WalkDuration 
        { get
            { 
                return new TimeSpan(0, 0, walk.Duration);
            }
        }
    }
}
