using System.ComponentModel.DataAnnotations;

namespace CVDynamicGenerator.WebApi.Entities.DTO
{
    public class DefaultRequest
    {
        public string lang { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string position { get; set; }
        public string profession { get; set; }
        public string cel { get; set; }
        public string email { get; set; }
        public string direction { get; set; }
        public List<link> links { get; set; }
        public List<job> jobs { get; set; }
        public List<education> educations { get; set; }
        public List<skill> skills { get; set; }
        public List<language> language { get; set; }
        public List<certification> cert { get; set; }
        public List<string> hobby { get; set; }
        public List<string> strength { get; set; }
        public List<string> award { get; set; }
    }

    public class certification
    {
        public string name { get; set; }
        public DateTime time { get; set; }
    }

    public class language
    {
        public string lang { get; set; }
        public int l { get; set; }
        public string level { get; set; }
    }

    public class skill
    {
        public string name { get; set; }
        public int level { get; set; }
    }

    public class education
    {
        public string institution { get; set; }
        public string courseName { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public List<string> activities { get; set; }
        public string location { get; set; }
    }

    public class link
    {
        public string icon { get; set; }
        public string liga { get; set; }
        public string name { get; set; }
    }
    public class job
    {
        public string enterprise { get; set; }
        public string position { get; set; }
        public DateTime startTime { get; set; }
        public bool isCurrently { get; set; }
        public DateTime endTime { get; set; }
        public List<string> activities { get; set; }
        public string location { get; set; }
        public string schedule { get; set; }
    }
}
