import { useMutation } from '@tanstack/react-query';
import { loginUser } from '../api/auth';
import { LoginRequest } from '../types/auth';

const useAuth = () => {
  
  
  
  const { mutate: login, isPending, error } = useMutation({
    mutationFn: (credentials: LoginRequest) => loginUser(credentials),
    onSuccess: (data: string) => {
      localStorage.removeItem('token');

      localStorage.setItem('token', data);
      // optionally store user data: localStorage.setItem('user', JSON.stringify(data.user));
    },
  });


  // Call /state and redirect if user is already logged in
  // const { isSuccess } = useQuery({
  //   queryKey: ['check-state'],
  //   queryFn: checkUserState,
  //   retry: false,
  //   staleTime: Infinity,
  // });

  // useEffect(() => {
  //   if (isSuccess) {
  //     navigate('/dashboard');
  //   }
  // }, [isSuccess, navigate]);
  const logout = () => {
    localStorage.removeItem('token');
    window.location.href = '/login';
  };

  const isAuthenticated = Boolean(localStorage.getItem('token'));

  return {
    login,
    logout,
    isAuthenticated,
    isPending,
    error,
  };
};

export default useAuth;
