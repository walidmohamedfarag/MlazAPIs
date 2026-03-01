//using System.ComponentModel.DataAnnotations;
//using System.Reflection;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace MlazAPIs.Services.Enum_Converter
//{
//    public class ConverterEnum : JsonConverter<ReportStatus>
//    {
//        public override ReportStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//        {
//            if(reader.TokenType == JsonTokenType.Number)
//            {
//                var value = reader.GetInt32();
//                return (ReportStatus)value;
//            }
//            else if(reader.TokenType == JsonTokenType.String)
//            {
//                var stringValue = reader.GetString();
//                if(Enum.TryParse<ReportStatus>(stringValue , true , out var result))
//                    return result;
//                foreach(var enumValue in typeof(ReportStatus).GetFields())
//                {
//                    var displayAttribute = enumValue.GetCustomAttributes<DisplayAttribute>();
//                    if (displayAttribute != null && displayAttribute. == stringValue)
//                        return (ReportStatus)enumValue.GetValue(null);
//                }
//            }
//        }

//        public override void Write(Utf8JsonWriter writer, ReportStatus value, JsonSerializerOptions options)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
