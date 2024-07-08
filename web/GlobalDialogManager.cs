using System.Collections.Concurrent;
using DdgAiProxy;

public class GlobalDialogManager(CustomClient customClient)
{
    private ConcurrentDictionary<Guid, DialogManager> _dialogs = new ConcurrentDictionary<Guid, DialogManager>();
    public Guid CreateNew(){
        DialogManager dialogManager = new DialogManager(customClient);
        Guid guid = Guid.NewGuid();
        _dialogs.TryAdd(guid, dialogManager);
        return guid;
    }
    public DialogManager GetManager(Guid id){
        return _dialogs[id];
    }
}