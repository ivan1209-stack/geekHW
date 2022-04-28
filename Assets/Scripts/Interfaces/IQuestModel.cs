using UnityEngine;

namespace Platformer
{
    
    public interface IQuestModel
    {
        bool TryComplete(GameObject actor);
    }
}
