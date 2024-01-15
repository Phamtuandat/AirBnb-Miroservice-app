namespace PropertyApi.Models;
public class PropertyLabel
{
      public string PropertyId { get; set; }
      public string LabelId { get; set; }
      public Label Label { get; set; }
      public Property Property { get; set; }
}