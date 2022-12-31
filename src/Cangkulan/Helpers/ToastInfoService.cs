using Microsoft.JSInterop;

namespace Cangkulan.Helpers
{
	public class ToastInfoService
	{
		IJSRuntime IJS;
		public ToastInfoService(IJSRuntime ijs)
		{
			IJS = ijs;
		}

		public async Task ShowInfo(string Message)
		{
			await IJS.InvokeVoidAsync("ShowInfo",Message);
		}
	}
}
