using System;
using System.Collections.Generic;
using System.Text;

namespace App.Common
{
    public class Message
    {
        #region Error

        public const string INVLID_DATA = "Error - Invalid data";
        public const string INTERNAL_ERROR = "Error - Internal";

        #endregion

        #region Product

        public const string PRODUCT_CREATE_SUCCESS = "Product created successfully!";
        public const string PRODUCT_CREATE_FAIL = "Error while creating productp!";

        public const string PRODUCT_UPDATE_SUCCESS = "Product updated successfully!";
        public const string PRODUCT_UPDATE_FAIL = "Error while updating productp!";

        public const string PRODUCT_DELETE_SUCCESS = "Product removed successfully!";
        public const string PRODUCT_DELETE_FAIL = "Error while removiing productp";

        #endregion

        #region Basket

        public const string ADD_PRODUCT_TO_BASKET_SUCCESS = "Product added to basket successfully!";
        public const string ADD_PRODUCT_TO_BASKET_FAIL = "Error while adding product to basket!";

        public const string REMOVE_PRODUCT_FROM_BASKET_SUCCESS = "Product removed from basket successfully!";
        public const string REMOVE_PRODUCT_FROM_BASKET_FAIL = "Error while removing product from basket!";

        public const string UPDATE_PRODUCT_QTY_IN_BASKET_SUCCESS = "Product quantity updated successfully!";
        public const string UPDATE_PRODUCT_QTY_IN_BASKET_FAIL = "Error while updating product quantity!";

        public const string PURCHASE_BASKET_SUCCESS = "Basket purchased successfully!";
        public const string PURCHASE_BASKET_FAIL = "Error while purchasing basket!";

        public const string DISCARD_BASKET_SUCCESS = "Basket discarded successfully!";
        public const string DISCARD_BASKET_FAIL = "Error while discarding basket!";

        public const string REGISTRATION_SUCCESS = "You registered successfully!";
        public const string REGISTRATION_FAIL = "Error while registering! Password should contain uppercase, lowercase and number";

        public const string USER_DOESNT_EXIST = "User doesn't exist!";
        public const string LOGIN_FAIL = "Login attempt failed!";

        #endregion
    }
}
