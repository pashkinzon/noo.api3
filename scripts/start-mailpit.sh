#!/bin/bash

export MP_SMTP_AUTH="noo-api:ksndc89sdhcd"

mailpit --smtp-tls-cert "$CERT_PATH" --smtp-tls-key "$KEY_PATH" --smtp-require-tls