using MVVM_ICOM_INOTIFY.InterfaceAdapter;

namespace MVVM_ICOM_INOTIFY.Application;

public class DownloadListGroups_US2
{
    
    private IDownloadList _downloadList;

    public DownloadListGroups_US2(IDownloadList downloadList)
    {
        this._downloadList = downloadList;
    }

    public List<string> DownloadGroupList()
    {
        return _downloadList.DownloadList();
    }

}