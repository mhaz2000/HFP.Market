import { Box } from "@mui/material";
import TutorialStepper from "../components/tutorial/TutorialStepper";
import TutorialVideo from "../components/tutorial/TutorialVideo";

export default function TutorialPage() {
  return (
    <Box sx={{ p: 4, minHeight: "100vh" }}>
      <TutorialStepper />
      <TutorialVideo />
    </Box>
  );
}
