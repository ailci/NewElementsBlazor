using Microsoft.AspNetCore.Components;

namespace ComponentsLibrary.Components;
public partial class ConfirmDialog
{
    [Parameter, EditorRequired] public string ConfirmTitle { get; set; } = string.Empty;
    [Parameter] public string ConfirmMessage { get; set; } = "Sind Sie sicher?";
    [Parameter] public EventCallback<bool> OnConfirmClicked { get; set; }
    private bool _showConfirm;
    private MarkupString _convertedConfirmMessage;

    public void Show()
    {
        _showConfirm = true;
    }
    public void Show(string message)
    {
        _showConfirm = true;
        ConfirmMessage = message;
        _convertedConfirmMessage = new MarkupString(Markdig.Markdown.ToHtml(message));
    }

    private async Task OnConfirmChange(bool isConfirmed)
    {
        _showConfirm = false;

        if (isConfirmed)
        {
            await OnConfirmClicked.InvokeAsync(isConfirmed);
        }
    }
}
