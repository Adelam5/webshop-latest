import Footer from "common/components/footer/Footer";
import Navigation from "common/components/navigation/Navigation";
import Checkout from "pages/Checkout";
import ConfirmEmail from "pages/ConfirmEmail";
import Home from "pages/Home";
import Login from "pages/Login";
import Register from "pages/Register";
import RegistrationSuccess from "pages/RegistrationSuccess";
import {
  createBrowserRouter,
  createRoutesFromElements,
  Outlet,
  Route,
  RouterProvider
} from "react-router-dom";
import RequireAuth from "RequireAuth";
import ProductDetails from "./pages/ProductDetails";
import PaymentSuccess from "pages/PaymentSuccess";
import PaymentFailed from "pages/PaymentFailed";
import UserProfile from "pages/UserProfile";

const App = () => {
  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path="/" element={<Root />}>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/register-success" element={<RegistrationSuccess />} />
        <Route path="/verify-email" element={<ConfirmEmail />} />

        <Route element={<RequireAuth allowedRoles={["User"]} />}>
          <Route path="/profile" element={<UserProfile />} />
          <Route path="/products/:productId" element={<ProductDetails />} />
          <Route path="/checkout" element={<Checkout />} />
          <Route path="/payment-success" element={<PaymentSuccess />} />
          <Route path="/payment-failed" element={<PaymentFailed />} />
        </Route>

        <Route element={<RequireAuth allowedRoles={["Admin"]} />}></Route>
      </Route>
    )
  );

  return (
    <div>
      <RouterProvider router={router} />
    </div>
  );
};

export default App;

const Root = () => {
  return (
    <>
      <Navigation />
      <Outlet />
      <Footer />
    </>
  );
};
