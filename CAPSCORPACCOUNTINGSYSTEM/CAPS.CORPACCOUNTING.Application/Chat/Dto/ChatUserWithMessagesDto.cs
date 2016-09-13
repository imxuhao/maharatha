using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Chat.Dto
{
    public class ChatUserWithMessagesDto : ChatUserDto
    {
        public List<ChatMessageDto> Messages { get; set; }
    }
}