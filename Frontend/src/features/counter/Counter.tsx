import { useStore } from "../../lib/hooks/useStore";
import { observer } from "mobx-react-lite";

const Counter = observer(function Counter() {
  const { counterStore } = useStore();
  return (
    <div>
      <h1>Counter</h1>
      <p>Count: {counterStore.count}</p>
      <button onClick={() => counterStore.increment()}>Increment</button>
      <button onClick={() => counterStore.decrement()}>Decrement</button>{" "}
    </div>
  );
});
export default Counter;
