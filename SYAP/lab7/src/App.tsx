import { RegistrationForm } from "./components/RegistrationForm";
import { Catalog } from "./components/Catalog";
import { AuthProvider, useAuth } from "./contexts/AuthContext";
import { ProductProvider } from "./contexts/ProductContext";

const AppContent = () => {
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) {
    return <RegistrationForm />;
  }

  return (
    <ProductProvider>
      <Catalog />
    </ProductProvider>
  );
};

function App() {
  return (
    <AuthProvider>
      <AppContent />
    </AuthProvider>
  );
}

export default App;
