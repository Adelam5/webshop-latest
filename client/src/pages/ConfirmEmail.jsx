import LoadingButton from "@mui/lab/LoadingButton";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import { useVerifyEmail } from "features/authentication/confirmation/useVerifyEmail";
import { useLocation } from "react-router-dom";

const ConfirmEmail = () => {
  const { search } = useLocation();
  const queryParams = new URLSearchParams(search);
  const token = queryParams.get("token");
  const userId = queryParams.get("userid");

  const { mutate: verify, isLoading, isSuccess, isError } = useVerifyEmail();

  return (
    <Container component="main" maxWidth="md">
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center"
        }}
      >
        <Typography
          component="h5"
          variant="h5"
          align="center"
          color="text.primary"
          gutterBottom
        >
          Email verification
        </Typography>

        <LoadingButton
          variant="contained"
          loadingPosition="start"
          loading={isLoading}
          onClick={() => verify({ token, userId })}
        >
          Verify Email
        </LoadingButton>
        {isSuccess && <div>Success</div>}
        {isError && <div>Error</div>}
      </Box>
    </Container>
  );
};
export default ConfirmEmail;
