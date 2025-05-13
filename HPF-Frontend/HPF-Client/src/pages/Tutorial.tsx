import { Box } from "@mui/material";
import TutorialStepper from "../components/tutorial/TutorialStepper";

export default function TutorialPage() {
  return (
    <Box sx={{ p: 4, bgcolor: "#f9f9f9", minHeight: "100vh" }}>
      <TutorialStepper />
    </Box>
  );
}
