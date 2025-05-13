import { useEffect } from 'react';

export function useKioskModeLock() {
  useEffect(() => {
    const enterFullscreen = async () => {
      const elem = document.documentElement;
      if (elem.requestFullscreen) await elem.requestFullscreen();
    };

    const reEnterFullscreen = () => {
      if (!document.fullscreenElement) enterFullscreen();
    };

    const disableContextMenu = (e: Event) => e.preventDefault();
    const disableKeyShortcuts = (e: KeyboardEvent) => {
      if (
        e.key === 'F12' ||
        e.key === 'Escape' ||
        (e.ctrlKey && e.shiftKey && ['I', 'J', 'C'].includes(e.key.toUpperCase())) ||
        (e.ctrlKey && e.key === 'U')
      ) {
        e.preventDefault();
        e.stopPropagation();
      }
    };

    document.addEventListener('contextmenu', disableContextMenu);
    document.addEventListener('keydown', disableKeyShortcuts);
    document.addEventListener('fullscreenchange', reEnterFullscreen);

    enterFullscreen(); // Try to enter fullscreen on load

    return () => {
      document.removeEventListener('contextmenu', disableContextMenu);
      document.removeEventListener('keydown', disableKeyShortcuts);
      document.removeEventListener('fullscreenchange', reEnterFullscreen);
    };
  }, []);
}
