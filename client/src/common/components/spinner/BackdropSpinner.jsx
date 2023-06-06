import Backdrop from "@mui/material/Backdrop";
import CircularProgress from "@mui/material/CircularProgress";

const BackdropSpinner = () => {
  return (
    <Backdrop sx={{ color: "#fff", zIndex: 1000 }} open={true}>
      <CircularProgress color="inherit" />
    </Backdrop>
  );
};
export default BackdropSpinner;
