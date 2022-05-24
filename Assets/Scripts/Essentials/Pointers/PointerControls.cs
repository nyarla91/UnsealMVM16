namespace Essentials.Pointers
{
    public static class PointerControls
    {
        private static PointerActions _actions;
        public static PointerActions Actions => _actions ?? Init();

        private static PointerActions Init()
        {
            _actions = new PointerActions();
            _actions.Enable();
            return _actions;
        }
    }
}