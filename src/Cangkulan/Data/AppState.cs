using Cangkulan.Helpers;

namespace Cangkulan.Data
{
    public class AppState
    {

        public event Action<string> OnProfileChange;
        public event Action<string> OnCompanyChange;
        public event Action<string, GeoLocation> OnLocationChange;
       

        public void RefreshProfile(string username)
        {
            ProfileStateChanged(username);
        }  
        
        public void RefreshCompany(string username)
        {
            CompanyStateChanged(username);
        } 
        
        public void SelectLocation(string username, GeoLocation loc)
        {
            LocationStateChanged(username,loc);
        }
      

      
        private void ProfileStateChanged(string username) => OnProfileChange?.Invoke(username);
        private void CompanyStateChanged(string username) => OnCompanyChange?.Invoke(username);
        private void LocationStateChanged(string username, GeoLocation loc) => OnLocationChange?.Invoke(username,loc);
      
    }
}
