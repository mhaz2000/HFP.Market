import {
    Box,
    Button,
    Step,
    StepLabel,
    Stepper,
    Typography,
  } from "@mui/material";
  import { AnimatePresence, motion } from "framer-motion";
  import { useState } from "react";
  
  const steps = [
    {
      label: "مرحله اول: انتخاب کالا",
      description: "ابتدا کالای مورد نظر را از قفسه ها بردارید.",
    },
    {
      label: "مرحله دوم: اسکن کالا",
      description: "سپس کالای خود را در مقابل دوربین قرار داده و اسکن کنید.",
    },
    {
      label: "مرحله سوم: پرداخت",
      description: "فاکتور خود را تایید و مبلغ را پرداخت نمایید.",
    },
  ];
  
  const MotionBox = motion(Box);
  
  export default function TutorialStepper() {
    const [activeStep, setActiveStep] = useState(0);
  
    const handleNext = () => {
      setActiveStep((prev) => Math.min(prev + 1, steps.length - 1));
    };
  
    const handleBack = () => {
      setActiveStep((prev) => Math.max(prev - 1, 0));
    };
  
    return (
      <Box dir="rtl" sx={{ maxWidth: 600, mx: "auto", mt: 10 }}>
        <Stepper activeStep={activeStep} alternativeLabel>
          {steps.map((step, index) => (
            <Step key={index}>
              <StepLabel>{step.label}</StepLabel>
            </Step>
          ))}
        </Stepper>
  
        <AnimatePresence mode="wait">
          <MotionBox
            key={activeStep}
            initial={{ opacity: 0, x: 40 }}
            animate={{ opacity: 1, x: 0 }}
            exit={{ opacity: 0, x: -40 }}
            transition={{ duration: 0.5 }}
            sx={{
              p: 3,
              mt: 3,
              bgcolor: "background.paper",
              borderRadius: 2,
              boxShadow: 3,
              textAlign: "right",
            }}
          >
            <Typography variant="h6" gutterBottom color="primary">
              {steps[activeStep].label}
            </Typography>
            <Typography variant="body1">{steps[activeStep].description}</Typography>
          </MotionBox>
        </AnimatePresence>
  
        <Box sx={{ display: "flex", justifyContent: "space-between", mt: 4 }}>
          <Button onClick={handleBack} disabled={activeStep === 0} variant="outlined">
            مرحله قبل
          </Button>
          <Button
            onClick={handleNext}
            disabled={activeStep === steps.length - 1}
            variant="contained"
          >
            مرحله بعد
          </Button>
        </Box>
      </Box>
    );
  }
  