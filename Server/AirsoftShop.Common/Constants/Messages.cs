namespace AirsoftShop.Common.Constants;

public static class Messages
{
    public const string RequiredFieldErrorMsg = "Полето е задължително.";
    public const string LengthErrorMsg = "Полето трябва да е между {1} и {2} символа.";
    public const string PasswordLengthErrorMsg = "Паролата трябва да е поне {1} символа.";
    public const string PasswordsNotMatchErrorMsg = "Паролите не съвпадат.";

    public const string InvalidEmailErrorMsg = "Моля въведете валиден и-мейл адрес.";

    public const string UnsuccessfulActionMsg = "Нещо се обърка, опитайте пак.";
    public const string SuccessfulDeleteMsg = "Изтриването беше успешно.";
    public const string SuccessfulEditMsg = "Промяната беше успешна.";

    public const string SuccessfulAddedItemMsg = "Успешно добавяне.";

    public const string InvalidCityMsg = "Избрали сте невалиден град";

    public const string FailedUserLoginMsg = "Невалиден потребител или парола";
    
    public const string UsernameExistsMsg = "Потребител с това име вече съществува";
    public const string EmailExistsMsg = "Потребител с този и-мейл вече съществува";
    public const string UserDoesNotExistMsg = "Потребител с това потребителско име/и-мейл не съществува!";
    public const string NotAuthorizedMsg = "Нямате права";
    public const string UserNotClientMsg = "Трябва да се регистрирате като клиент!";
    public const string UserNotDealerMsg = "Трябва да се регистрирате като продавач!";

    public const string SuccessfulOrderMsg = "Успешна поръчка!";
    
    public const string InvalidImage = "Необходимо е да качите снимка!";
    public const string InvalidImageExtension = "Снимката е с невалидно разширение! Поддържани са 'jpg', 'jpeg', 'png' и 'gif'.";
    public const string InvalidSubcategoryErrorMsg = "Избрали сте невалидна подкатегория!";
    public const string InvalidProduct = "Този продукт не съществува!";
    public const string InvalidField = "Това поле не съществува!";
    public const string InvalidCourier = "Избрали сте невалиден куриер!";
    public const string InvalidRemoveAction = "Не може да премахнете този артикул!";
}