import { useRoutes } from 'react-router-dom';
import routes from './routes/routes';
import { ToastContainer } from 'react-toastify';
import { useKioskModeLock } from './hooks/useKioskModeLock';

function App() {
  // useKioskModeLock();
  const routing = useRoutes(routes);

  return (
    <div>
      {routing}
      <ToastContainer rtl position="bottom-right" toastStyle={{
        fontFamily: '"IRANSans", "Vazirmatn", "Roboto", "Arial", sans-serif',
      }} />
    </div>
  );
}

export default App;
