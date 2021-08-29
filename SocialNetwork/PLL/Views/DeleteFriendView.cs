using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class DeleteFriendView
    {
        UserService userService;
        public DeleteFriendView(UserService userService)
        {
            this.userService = userService;
        }

        public void Show(User user)
        {
            Console.WriteLine("Мои друзья: ");
            var friends = userService.GetFriendsByUserId(user.Id);
            foreach (var item in friends)
            {
                Console.WriteLine("Email: {0}. Имя: {1}. Фамилия: {2}", item.Email, item.FirstName, item.LastName);
            }

            Console.Write("Введите почтовый адрес друга, которого необходимо удалить: ");
            string friendEmail = Console.ReadLine();

            try
            {
                int friendId = userService.FindByEmail(friendEmail).Id;
                userService.DeleteFriend(user.Id, friendId);

                SuccessMessage.Show("Друг успешно удален!");
            }

            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден!");
            }

            catch (Exception)
            {
                AlertMessage.Show("Ошибка!");
            }


        }

    }
}
