using Manatee.Trello;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using System;
using System.Linq;


// Документация https://gregsdennis.github.io/Manatee.Trello/usage/getting-started.html
namespace ManateeTrelloTest
{
    //Попытка использовать Power-Ups
    public class CustomFieldsPowerUp : PowerUpBase
    {
        internal const string PluginId = "56d5e249a98895a9797bebb9";

        private static bool _isRegistered;

        private CustomFieldsPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
        : base(json, auth) { }

        internal static void Register()
        {
            if (!_isRegistered)
            {
                _isRegistered = true;
                TrelloConfiguration.RegisterPowerUp(PluginId, (j, a) => new CustomFieldsPowerUp(j, a));
            }
        }
    }


    
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //Авторизация, для которой нужен ключ разработчика https://trello.com/app-key
            TrelloAuthorization.Default.AppKey = "16a605bc7686b56000bac3a0c88ad11c";                            
            TrelloAuthorization.Default.UserToken = "09d5a1bef5a2aeb437e783d2e3a5066eea293750d98ab64a03a1c7cf335ccf27";

            //Создание доски и подключение к нужно нам
            ITrelloFactory factory = new TrelloFactory();
            var board = factory.Board("hYFRk7rz");
            await board.Refresh();                        //Обязательно, после загрузки данных

            var list = board.Lists.FirstOrDefault();      //Подключение к спискам (колонкам в Trello)
            await list.Refresh();
            var card = list.Cards.FirstOrDefault();       //Подключение к карточкам 
            await card.Refresh();    

            /*//Выводит название списков
            foreach (var listq in board.Lists)
            {
                Console.WriteLine(listq);
                await list.Refresh();
                 Console.WriteLine(" ");    
                     //Выводит название карточек первого списка*/
            foreach (var cardq in list.Cards)
            {
     
                    Console.WriteLine("[" + cardq.Id + "]");
                    Console.WriteLine("[" + cardq.Name + "]");
                    Console.WriteLine("[" + cardq.Description + "]");
                    Console.WriteLine("[" + cardq.CreationDate + "]");

                }

                Console.WriteLine(" ");         
            
       
            
        }
    }
}