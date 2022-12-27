namespace Cangkulan.Data
{
    public class AppState
    {

        public event Action<string> OnProfileChange;
        public event Action<string> OnCompanyChange;
       

        public void RefreshProfile(string username)
        {
            ProfileStateChanged(username);
        }  
        
        public void RefreshCompany(string username)
        {
            CompanyStateChanged(username);
        }
      

      
        private void ProfileStateChanged(string username) => OnProfileChange?.Invoke(username);
        private void CompanyStateChanged(string username) => OnCompanyChange?.Invoke(username);
      
    }
}
