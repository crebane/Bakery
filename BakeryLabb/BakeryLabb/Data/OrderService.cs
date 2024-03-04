using BakeryLabb.Classes;

namespace BakeryLabb.Data;

public class OrderService
{
    public UserInformation UserInformation { get; private set; } = new UserInformation();

    public void SetOrderInfo(UserInformation userInformation)
    {
        UserInformation = userInformation;
    }
}
