# WindowAnimation - анимация окна

В данной системе окон анимация окна отделена от окна, она представлена отдельным `ScriptableObject`-классом `WindowAnimation` от которого наследованы:

- `WindowActivationAnimation` - анимация активации.
- `WindowDeactivationAnimation` - анимация деактивации.

У любого окна есть два поля для установки анимаций по умолчанию:

- `DefaultWindowActivationAnimation` - анимация активации окна по умолчанию.
- `DefaultWindowDeactivationAnimation` - анимация деактивации окна по умолчанию.

![image.png](image%201.png)

Вы обязаны заполнить эти два поля до того как будете использовать окна. В пакете уже поставляются анимации простого мгновенного переключения окна:

- `TogglingActivationWindowAnimation` - мгновенно переключит окно в активное состояние.
- `TogglingDeactivationWindowAnimation` - мгновенно переключит окно в инактивное состояние.

Для создания ассетов этих анимаций в проекте, в окне `Project` используйте контекстное меню `Create` → `Configs` → `WindowsSystem` → `Animations` → `Toggling` и выберите `Activation` или `Deactivation`.

![image.png](image%202.png)

После этого вы можете назначить их нужным окнам.

![image.png](image%203.png)

Для создания новой анимации вы должны создать новый класс, наследовать его от `WindowActivationAnimation` или `WindowDeactivationAnimation`, переопределить в нем метод `AnimateEnumerator` и написать в нем код представляющий вашу анимацию.

```csharp
using System.Collections;
using Azur.WindowsSystem;
using Azur.WindowsSystem.Animations;
using UnityEngine;

namespace MagicTeam.WindowsSystem.Animations
{
    [CreateAssetMenu(fileName = "FadingActivationWindowAnimation", menuName = "Configs/WindowsSystem/Animations/Fading/Activation")]
    public class FadingWindowActivationAnimation : WindowActivationAnimation
    {
        [SerializeField] private float _duration = 1f;
        
        // Переопределили данный метод, чтобы написать свою анимацию.
        public override IEnumerator AnimateEnumerator(Window window, bool useUnscaledTime)
        {
            var canvasGroup = (window as WindowWithCanvasGroup).CanvasGroup;

            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += (useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime) / _duration;
                yield return null;
            }
            
            canvasGroup.alpha = 1f;
        }
    }
}
```

Обратите внимание, что метод `AnimateEnumerator` принимает параметр `useUnscaledTime`, который вы должны корректно обработать в своей анимации. Если этот параметр равен `true`, то вы должны использовать такой подсчет времени анимации, который не будет зависеть от `Time.timeScale`, например использовать `Time.unscaledDeltaTime`.

<aside>
💡

Вам не обязательно создавать именно пару (Activation и Deactivation) анимаций, вы можете создать только одну их них и использовать ее в окнах.

</aside>