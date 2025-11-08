import * as React from 'react';
import SvgIcon from '@mui/material/SvgIcon';
import type { SvgIconProps } from '@mui/material';



/** Логотип сайту */
export const SitemarkIcon = (props: SvgIconProps) => (
  <SvgIcon {...props} sx={{ fontSize: 48, color: 'primary.main', mb: 2 }}>
    <path d="M12 2L2 7l10 5 10-5-10-5zm0 10L2 7v10l10 5 10-5V7l-10 5z" />
  </SvgIcon>
);

/** Google іконка */
export const GoogleIcon = (props: SvgIconProps) => (
  <SvgIcon {...props}>
    <path d="M21.35 11.1h-9.18v2.93h5.53c-.24 1.45-1.41 4.27-5.53 4.27-3.33 0-6.04-2.76-6.04-6.15s2.71-6.15 6.04-6.15c1.89 0 3.16.81 3.88 1.5l2.65-2.55C17.6 3.49 15.15 2 12 2 6.48 2 2 6.48 2 12s4.48 10 10 10c5.77 0 9.68-4.04 9.68-9.68 0-.65-.07-1.13-.33-1.52z"/>
  </SvgIcon>
);

/** Facebook іконка */
export const FacebookIcon = (props: SvgIconProps) => (
  <SvgIcon {...props}>
    <path d="M22 12c0-5.52-4.48-10-10-10S2 6.48 2 12c0 5 3.66 9.13 8.44 9.88v-6.99h-2.54V12h2.54V9.8c0-2.5 1.49-3.89 3.77-3.89 1.09 0 2.23.2 2.23.2v2.46h-1.26c-1.24 0-1.63.77-1.63 1.56V12h2.78l-.44 2.89h-2.34v6.99C18.34 21.13 22 17 22 12z"/>
  </SvgIcon>
);
