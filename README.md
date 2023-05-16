# GiveawayInsider
Check game giveaways and get email notifications on specific ones with GiveawayInsider.

Demo:
https://giveawayinsiderclient20230424.azurewebsites.net
(When loaded use Ctrl+F5 to clean browsers cache, cache overflow can cause some errors!)

Written with help of Blazor WASM, Web API, SendGrid for emailing users. You can check game giveaways of different type on diffirent platforms. 
Registered users can create notifications (up to 5) on specific search of giveaways and will be notified every day at 12:00 (GMT+2).
Information about giveaways used from GamePowerAPI: https://www.gamerpower.com/api-read

To configure on local host:
- In GiveAwayInsider_API/Helper/ApiKeyProvider.cs and GiveAwayInsider_Nofifier/Program.cs specify yours custom api key (instead of "your api key");
- In GiveAwayInsider_API/Program.cs in email service add your SendGrid api key;
- Start GiveAwayInsider_API project to migrate database (if not migrated, update database via pm);
- Done, you can start all 3 projects (GiveAwayInsider_API, GiveAwayInsider_Client, GiveAwayInsider_Notifier) and use the web apps;
- (When client loaded use Ctrl+F5 to clean browsers cache, cache overflow can cause some errors!);
