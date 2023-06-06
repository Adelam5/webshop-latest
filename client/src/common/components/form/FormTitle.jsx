import Avatar from "@mui/material/Avatar";
import Typography from "@mui/material/Typography";

const FormTitle = ({ title, icon }) => {
  return (
    <>
      {icon && <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>{icon}</Avatar>}
      <Typography component="h1" variant="h5">
        {title}
      </Typography>
    </>
  );
};
export default FormTitle;
