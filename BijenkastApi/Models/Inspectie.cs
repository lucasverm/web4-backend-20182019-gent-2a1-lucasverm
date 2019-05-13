using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BijenkastApi.Models
{
    public class Inspectie
    {
        public int id { get; set; }
        public int dag { get; set; }
        public int maand { get; set; }
        public int jaar { get; set; }
        public string notitie { get; set; }
        public bool eitjes { get; set; }
        public bool larven { get; set; }
        public bool poppen { get; set; }
        public int ramenmetbijen { get; set; }
        public bool moeraanwezig { get; set; }
        public bool honing { get; set; }
        public bool stuifmeel { get; set; }

        [ForeignKey("Kast")]
        public int kastId { get; set; }

        public Inspectie(int dag, int maand, int jaar, string notitie)
        {
            this.dag = dag;
            this.maand = maand;
            this.jaar = jaar;
            this.notitie = notitie;
        }

        public Inspectie()
        {
        }
    }
}