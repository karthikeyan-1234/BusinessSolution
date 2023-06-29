using CommonLibrary.Models;

namespace CommonLibrary.Models
{
    public class Inventory : BaseModel
    {
        public int id { get; set; }
        public string ?item_id { get; set; }
        public float qty { get; set; }
        public DateTime last_edit_at { get; set; }
    }
}
