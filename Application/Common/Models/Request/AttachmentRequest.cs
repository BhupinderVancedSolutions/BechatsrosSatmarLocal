namespace Application.Common.Models.Request
{
    public class AttachmentRequest
    {
        public byte[] Content
        {
            get;
            set;
        }    
        public string Type
        {
            get;
            set;
        }        
        public string Filename
        {
            get;
            set;
        }        
        public string Disposition
        {
            get;
            set;
        }        
        public string ContentId
        {
            get;
            set;
        }        
    }
}
