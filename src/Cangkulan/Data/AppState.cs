using Cangkulan.Helpers;

namespace Cangkulan.Data
{
	public class AppState
	{

		public event Action<string> OnProfileChange;
		public event Action<string> OnCompanyChange;
		public event Action<string, GeoLocation> OnLocationChange;
		public event Action<string> OnJobChange;
		public event Action<string> OnJobCandidateChange;
		public event Action<string> OnProjectChange;
		public event Action<string> OnProjectBidderChange;
		public event Action<string> OnBookmarkedJobChange;
		public event Action<string> OnBookmarkedFreelancerChange;

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
			LocationStateChanged(username, loc);
		}

		public void RefreshJob(string username)
		{
			JobStateChanged(username);
		}
		public void RefreshJobCandidate(string username)
		{
			JobCandidateStateChanged(username);
		}
		public void RefreshProject(string username)
		{
			ProjectStateChanged(username);
		}

		public void RefreshProjectBidder(string username)
		{
			ProjectBidderStateChanged(username);
		}
		public void RefreshBookmarkedJob(string username)
		{
			BookmarkedJobStateChanged(username);
		}
		public void RefreshBookmarkedFreelancer(string username)
		{
			BookmarkedFreelancerStateChanged(username);
		}

		private void ProfileStateChanged(string username) => OnProfileChange?.Invoke(username);
		private void CompanyStateChanged(string username) => OnCompanyChange?.Invoke(username);
		private void LocationStateChanged(string username, GeoLocation loc) => OnLocationChange?.Invoke(username, loc);
		private void JobStateChanged(string username) => OnJobChange?.Invoke(username);
		private void JobCandidateStateChanged(string username) => OnJobCandidateChange?.Invoke(username);
		private void ProjectStateChanged(string username) => OnProjectChange?.Invoke(username);
		private void ProjectBidderStateChanged(string username) => OnProjectBidderChange?.Invoke(username);
		private void BookmarkedJobStateChanged(string username) => OnBookmarkedJobChange?.Invoke(username);
		private void BookmarkedFreelancerStateChanged(string username) => OnBookmarkedFreelancerChange?.Invoke(username);
	}
}
