using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Exceptions
{
    public  class RepoException : Exception
    {
        public RepoException() { }

        public Exception LocationException() 
        {
            throw new Exception("Repository | Have not Location");
        }
        public Exception AssignmentException()
        {
            throw new Exception("Repository | Have not Assignment");
        }
        public Exception AssetException()
        {
            throw new Exception("Repository | Have not Assignment");
        }
        public Exception UserException()
        {
            throw new Exception("Repository | Have not User");
        }
        public Exception UserRoleException()
        {
            throw new Exception("Repository | Have not UserRole");
        }
        public Exception RoleException()
        {
            throw new Exception("Repository | Have not Role");
        }
        public Exception CreateUserException()
        {
            throw new Exception("Repository | Create user fail");
        }
        public Exception UpdateUserxception()
        {
            throw new Exception("Repository | Update user fail");
        }
        public Exception UseNamerException()
        {
            throw new Exception("Repository | First name or Last name is not valid");
        }
        public Exception CateNameException()
        {
            throw new Exception("Repository | Category Name is used");
        }
        public Exception CatePrefixException()
        {
            throw new Exception("Repository | Prefix is used");
        }
        public Exception CreateCateException()
        {
            throw new Exception("Repository | Create category fail");
        }
        public Exception RetuenRequestExistsException()
        {
            throw new Exception("Repository | Return request have exists");
        }
        public Exception AssignToUserException()
        {
            throw new Exception("Repository | Have not this Assign To User");
        }
        public Exception AssignByAdminException()
        {
            throw new Exception("Repository | Have not this Assign By Admin");
        }
        public Exception NotAvailableException()
        {
            throw new Exception("Repository | This asset is not available");
        }
        public Exception CreateAssignmentException()
        {
            throw new Exception("Repository | Create assignment fail");
        }
        public Exception DeleteAssigmentException()
        {
            throw new Exception("Repository | Delete assignment fail");
        }
        public Exception NotValidException()
        {
            throw new Exception("Repository | State is not valid");
        }
        public Exception ErrorAssetException()
        {
            throw new Exception("Repository | Have not this old asset");
        }
        public Exception CheckDateAssignmentException()
        {
            throw new Exception("Repository | Assigned Date is smaller than Today");
        }
        public Exception UpdateAssignmentException()
        {
            throw new Exception("Repository | Update assignment fail");
        }
        public Exception ChangeStateException()
        {
            throw new Exception("Repository | Change state assignment fail");
        }
        public Exception AssignmentExistsException()
        {
            throw new Exception("Repository | This assignment have a return request");
        }
        public Exception AssetIsAssignException()
        {
            throw new Exception("Repository | This asset is Assigned");
        }
        public Exception SateAsignmentException()
        {
            throw new Exception("Repository | State must be waiting for acceptance");
        }
        public Exception ChangeStateReturnException()
        {
            throw new Exception("Repository | Change state return request fail");
        }
        public Exception AssignmentRetyrningException()
        {
            throw new Exception("Repository | This assignment is returing");
        }
        public Exception RequestUserEsistsException()
        {
            throw new Exception("Repository | Request User not exists");
        }
        public Exception AssignmentSateExistsException()
        {
            throw new Exception("Repository | This assignment have a return request");
        }
        public Exception CreateReturnRequestException()
        {
            throw new Exception("Repository | Create Return Request fail");
        }
        public Exception AcceptUserException()
        {
            throw new Exception("Repository | Have not this accept user");
        }
        public Exception StateAssignIsAcceptedException()
        {
            throw new Exception("Repository | State must be accepted");
        }
        public Exception StateAssignIsCompetedException()
        {
            throw new Exception("Repository | State is completed");
        }
        public Exception CaterogytException()
        {
            throw new Exception("Repository | Have not this category");
        }
        public Exception UpdateAssetException()
        {
            throw new Exception("Repository | Update asset fail");
        }
        public Exception HaveAssignException()
        {
            throw new Exception("Repository | Have assignment");
        }
        public Exception DeleteAssetException()
        {
            throw new Exception("Repository | Delete asset fail");
        }
        public Exception CreateAssetException()
        {
            throw new Exception("Repository | Create Asset Fail");
        }    
    }
}
