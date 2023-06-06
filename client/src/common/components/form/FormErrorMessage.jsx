import Typography from "@mui/material/Typography";

const FormErrorMessage = ({ message }) => {
  return (
    <Typography variant="p" style={{ color: "tomato" }}>
      {message}
    </Typography>
  );
};
export default FormErrorMessage;
