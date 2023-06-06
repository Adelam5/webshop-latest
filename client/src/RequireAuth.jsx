import Spinner from "common/components/spinner/Spinner";
import { useCurrentUser } from "features/authentication/common/useCurrentUser";
import { Navigate, Outlet, useLocation } from "react-router-dom";

const RequireAuth = ({ allowedRoles }) => {
  const { data: user, isLoading } = useCurrentUser();

  const location = useLocation();

  if (isLoading) {
    return <Spinner />;
  }

  const hasAllowedRole = allowedRoles.some((role) =>
    user?.roles?.includes(role)
  );

  return hasAllowedRole ? (
    <Outlet />
  ) : (
    <Navigate to="/login" state={{ from: location }} replace />
  );
};

export default RequireAuth;
