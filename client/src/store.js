import { create } from "zustand";
import { devtools } from "zustand/middleware";

const initialState = {
  activeStep: 0
};

let store = (set) => ({
  ...initialState,

  setActiveStep: (activeStep) =>
    set(() => ({ activeStep }), false, "setActiveStep"),

  reset: () => set(initialState, false, "reset")
});

store = devtools(store);

export const useStore = create(store);
