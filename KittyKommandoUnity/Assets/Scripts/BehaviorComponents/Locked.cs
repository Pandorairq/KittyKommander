using Action;
using UnityEngine;
using UnityEngine.Events;

namespace BehaviorComponents
{
    public class Locked : MonoBehaviour
    {
        public UnityEvent onUnlocked;
        [SerializeField] private KeyColor keyColor;
        public void Unlock(Interactor interactor)
        {
            var items = interactor.GetComponent<Inventory>().GetItems();
            foreach (var item in items)
            {
                var keyComponent = item?.GetComponent<Key>();
                if (keyComponent && keyComponent.GetKeyColor() == keyColor)
                {
                    onUnlocked.Invoke();
                }
            }
        }
    }
}
