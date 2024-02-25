using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants {
    public static class Messages {

        //general messages
        public static string Added { get; } = " added successfully.";
        public static string Deleted { get; } = " deleted successfully.";
        public static string Updated { get; } = " updated successfully.";


        //auth manager
        public static string UserRegistered { get; } = "Registered successfully.";
        public static string WrongPasswordOrEmail { get; } = "Wrong password or email.";
        public static string SuccessfulLogin { get; } = "Successfull login.";
        public static string AccessTokenCreated { get; } = "Access token created.";
        public static string UserAlreadyExists { get; } = "User already exists.";


        //category manager
        public static string CategoryAlreadyExists { get; } = "Category already exists.";
        public static string CategoryAdded { get; } = "Category" + Added;
        public static string CategoryDeleted { get; } = "Category" + Deleted;
        public static string CategoryUpdated { get; } = "Category" + Updated;


        //product image manager
        public static string ImageAdded { get; } = "Image" + Added;
        public static string ImageDeleted { get; } = "Image" + Deleted;
        public static string ImagesAdded { get; } = "Images" + Added;
        public static string ImageNotFound { get; } = "Image Not Found.";
        public static string ProductImageCountExceeded { get; } = "Product image count exceeded.";


        //product manager
        public static string ProductAdded { get; } = "Product" + Added;
        public static string ProductDeleted { get; } = "Product" + Deleted;
        public static string ProductUpdated { get; } = "Product" + Updated;


        //customer manager
        public static string CustomerAdded { get; } = "Customer" + Added;
        public static string CustomerDeleted { get; } = "Customer" + Deleted;
        public static string CustomerUpdated { get; } = "Customer" + Updated;
        public static string InformationUpdated { get; } = "Information updated";
        public static string UserDoesntExists { get; } = "User doesn't exist";


        //process manager
        public static string ProductProcessed { get; } = "Product processed successfully";
        public static string ProductAlreadyProcessed { get; } = "Product is already processed in given interval";
        public static string CustomerHasNoNationalIdentity { get; } = "You need to add your national identity from your profile to processed a product";


        //user manager
        public static string PasswordError { get; } = "Password error";
        public static string PasswordUpdated { get; } = "Password" + Updated;





        public static string AuthorizationDenied { get; } = "Authorization denied";




    }

}
