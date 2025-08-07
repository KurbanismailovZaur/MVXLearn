# SwitchingStrategy - тип переключения окон

Когда вы используете методы `WindowsManager.DeactivateUpTo` или `WindowsManager.SwitchWindows`, вы можете указать стратегию переключения окон. Из коробки сушествуют три стратегии:

- **`Sequentially` - последовательно деактивирует и активирует окна.**
- **`Concurrently` - параллельно деактивирует окна и после этого активирует другое окно.**
- **`Immediately` - параллельно деактивирует и активирует окна.**

Вы можете создать ассеты этих стратегий с помощью меню `Create` → `Configs` → `WindowsSystem` → `Switching` и выберите нужную вам.

## Создание новой стратегии

Для создания новой стратегии создайте новый скриптбл-класс, наследуйте его от `WindowsSwitchingStrategy` и реализуйте метод `SwitchEnumerator`, как в примере ниже.

```jsx
using System.Collections;
using System.Collections.Generic;
using Azur.WindowsSystem.Animations;
using UnityEngine;

namespace Azur.WindowsSystem.Switching
{
    [CreateAssetMenu(fileName = "SequentiallyWindowsSwitchingStrategy", menuName = "Configs/WindowsSystem/Switching/SequentiallyWindowsSwitchingStrategy")]
    public class SequentiallyWindowsSwitchingStrategy : WindowsSwitchingStrategy
    {
        public override IEnumerator SwitchEnumerator(List<Window> activatedWindows, Window deactivationWindow, 
            Window activationWindow, bool useUnscaledTime, int canvasSortOrderOffset, 
            WindowDeactivationAnimation windowDeactivationAnimation, WindowActivationAnimation windowActivationAnimation,
            MonoBehaviour coroutineOwner)
        {
		        // Если есть окно до которого (вкллючительно) надо деактивировать все остальные, то..
            if (deactivationWindow != null)
            {
		            // Получаем его индекс в списке активированных окон.
                var fromIndex = activatedWindows.IndexOf(deactivationWindow);
                
                // Деактивируем все окна до него (включительно)
                for (var i = activatedWindows.Count - 1; i >= fromIndex; i--)
                {
                    var lastWindow = activatedWindows[i];
                    activatedWindows.RemoveAt(i);
                    yield return lastWindow.Deactivate(useUnscaledTime, windowDeactivationAnimation, coroutineOwner);

										// Обязательно включайте рейкастер на предыдущем окне после деактивации текущего.
                    if (i - 1 >= 0)
                        activatedWindows[i - 1].GraphicRaycaster.enabled = true;
                }   
            }

						// Если нет окна для активации, то завершаем метод.
            if (activationWindow == null) 
                yield break;

						// Активируем окно activationWindow.
						// Обязательно выключайте рейкастер на текущем окне перед активацией нового.
            if (activatedWindows.Count > 0)
                activatedWindows[^1].GraphicRaycaster.enabled = false;
        
            activatedWindows.Add(activationWindow);
            activationWindow.Canvas.sortingOrder = canvasSortOrderOffset + activatedWindows.Count - 1;
            yield return activationWindow.Activate(useUnscaledTime, windowActivationAnimation, coroutineOwner);
        }
    }
}
```