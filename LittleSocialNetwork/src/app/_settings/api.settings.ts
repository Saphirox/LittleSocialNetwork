
export class ApiSettings {
   public static url = 'http://localhost:55872';

   public static signin = ApiSettings.url + '/api/account/signin';
   public static createMessage = ApiSettings.url + '/api/messages/create-message';
   public static getCurrentUserConversations = ApiSettings.url + '/api/messages/get-conversations';

   // For signalR
   public static singleChatHub =  ApiSettings.url + '/chat';

   public static getLongMessages(otherId: number) {
        return ApiSettings.url + `/api/messages/get-messages?otherId=${otherId}&take=10&skip=0`;
   }
}
