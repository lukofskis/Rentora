/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    screens: {
      sm: "480px",
      md: "768px",
      lg: "1020px",
      xl: "1440px",
    },
    extend: {
      fontFamily: {
        sans: ["Poppins", "sans-serif"],
      },
    },
  },
  plugins: [],
};


// export default {
//   content: [
//     "./index.html",                  // Pagrindinis HTML failas
//     "./src/**/*.{js,ts,jsx,tsx}",    // Visi JS, TS, JSX, TSX failai po `src/`
//   ],
//   theme: {
//     extend: {},
//   },
//   plugins: [],
// };
