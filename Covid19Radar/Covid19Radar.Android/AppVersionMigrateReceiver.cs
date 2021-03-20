using Android.App;
using Android.Content;
using AndroidX.Work;
using CommonServiceLocator;
using Covid19Radar.Services.Logs;
using Xamarin.ExposureNotifications;

namespace Covid19Radar.Droid
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionMyPackageReplaced })]
    public class AppVersionMigrateReceiver : BroadcastReceiver
    {
        private static readonly string WorkerName = "exposurenotification";

        private readonly ILoggerService loggerService = ServiceLocator.Current.GetInstance<ILoggerService>();

        public override void OnReceive(Context context, Intent intent)
        {
            loggerService.StartMethod();

            if (intent.Action != Intent.ActionMyPackageReplaced)
            {
                return;
            }

            CancelWorker(WorkManager.GetInstance(context));

            InitializeExposureNotification();

            loggerService.EndMethod();
        }

        private void CancelWorker(WorkManager workManager)
        {
            loggerService.StartMethod();

            workManager.CancelUniqueWork(WorkerName);

            loggerService.EndMethod();
        }

        private void InitializeExposureNotification()
        {
            loggerService.StartMethod();

            ExposureNotification.Init();

            loggerService.EndMethod();
        }
    }
}
