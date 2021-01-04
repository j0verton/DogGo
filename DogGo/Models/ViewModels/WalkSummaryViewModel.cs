using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkSummaryViewModel
    {
        public Walk walk;

        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\}")]
        public TimeSpan WalkDuration 
        { get
            { 
                return new TimeSpan(0, 0, walk.Duration);
            }
        }


    }
}
