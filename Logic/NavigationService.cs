using System;
using System.Collections.Generic;

namespace Team3_ProjectB
{
    public static class NavigationService
    {
        private static Stack<Action> navigationStack = new Stack<Action>();

        public static void Navigate(Action screen)
        {
            navigationStack.Push(screen);
            screen();
        }

        public static void GoBack()
        {
            if (navigationStack.Count > 1)
            {
                navigationStack.Pop();

                navigationStack.Peek().Invoke();
            }
        }

        public static void Clear()
        {
            navigationStack.Clear();
        }
    }
}

