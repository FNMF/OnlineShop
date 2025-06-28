using API.Domain.Enums;

namespace API.Api.Models
{
    /*
     * 这是用于API层传入的Dto，减去了各种UUID以及各种类型
     * 类型会在后续的Service层中实现，需要鉴权后才能写入，比如ReceiverType之类的
     * 如果API层不独立DTO会导致可能的越权处理，以及信息问题
     * 在后续Service层中会有CommandDto用于保存除了UUID之外的各种信息，UUID则是通过Service生成
     */
    public class CreateNotificationInputDto
    {
        public byte[]? ReceivedUuid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
