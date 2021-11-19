using System;
using System.Collections.Generic;
using System.Text;

namespace BreathTechRelease.Models
{
    public class ListViewTemplate
    {

        public string Name { get; set; }
        public string Desc { get; set; }

        public string Icon { get; set; }
        public int Count { get; set; }

        public int Id { get; set; }


        //Bio data members

        public string BioImage { get; set; }

        public string BioText { get; set; }
    
        public string BioName { get; set; }

        public string Source { get; set; }



        public string Heading { get; set; }

        public string IntroText { get; set; }
        public string Url_Text { get; set; }

        public string Url_Vid { get; set; }
        public string Url_Aud { get; set; }

    }


}
