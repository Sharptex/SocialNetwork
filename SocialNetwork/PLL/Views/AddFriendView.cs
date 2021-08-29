using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class AddFriendView
    {
        UserService userService;
        public AddFriendView(UserService userService)
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

            Console.Write("Введите почтовый адрес нового друга: ");
            string userEmail = Console.ReadLine();

            try
            {
                userService.AddFriend(new UserAddFriendData() { UserId = user.Id, FriendEmail = userEmail });

                SuccessMessage.Show("Друг успешно добавлен!");
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
