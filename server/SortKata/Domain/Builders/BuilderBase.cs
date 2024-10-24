namespace SortKata.Domain.Builders {
    public abstract class BuilderBase<T> where T : new() {
        protected T? _t;

        protected BuilderBase() {
            this._t = default;
        }

        public BuilderBase<T> New() {
            this._t = new T();
            return this;
        }

        public T Finalize() {
            this.CheckInstanciate();
            return this._t!;
        }

        protected void CheckInstanciate() {
            if (this._t == null) {
                this.New();
            }
        }
    }
}