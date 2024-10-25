using MVVM_ICOM_INOTIFY.InterfaceAdapter;

namespace MVVM_ICOM_INOTIFY.Application;

public class DownloadGroupHeadline
{
    private IDownloadHeadlines _downloadHeadlines;

    public DownloadGroupHeadline(IDownloadHeadlines downloadHeadlines)
    {
        this._downloadHeadlines = downloadHeadlines;
    }

    
    public List<string> DownloadHeadlineList(string groupName)
    {
        return _downloadHeadlines.DownloadNewsHeadlines(groupName);
    }
}